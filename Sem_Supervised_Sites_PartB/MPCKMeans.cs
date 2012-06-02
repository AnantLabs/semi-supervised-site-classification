#region Author © Verein Konstantin, 2012
//KocT9H@gmail.com
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accord.Math;

namespace Sem_Supervised_Sites_PartB
{
    

    class MPCKMeans
    {
        public struct cluster
        {
            public double[] clusterCentroid;
            public double[,] learningMatrix;
            public LinkedList<double[]> relatedPoints;
            public LinkedList<double[]> localMustLink;
            public LinkedList<double[]> localCannotLink;
        }


        private cluster[] clust;
        private int clusterNumber;
        private double mustViolation;
        private double cannotViolation;
        private LinkedList<double[,]> mustLinkConstraints;
        private int word_number;
        private LinkedList<double[]> initialMatrix;

        
        #region Constructor
        public MPCKMeans(int clust_num, double[,] matrix, LinkedList<double[,]> mLC, double mViol, double cViol)
        {
            this.clusterNumber = clust_num;
            this.mustViolation = mViol;
            this.cannotViolation = cViol;
            this.clust = new cluster[clust_num];
            this.mustLinkConstraints = new LinkedList<double[,]>(mLC);
           
            word_number=matrix.GetLength(1);
            
            //update initial matrix
            LinkedList<double[]> iniMatr = new LinkedList<double[]>();
            this.initialMatrix = iniMatr;

            for (int b = 0; b < matrix.GetLength(0); b++)
            {
                double[] node = new double[word_number];
                for (int k = 0; k < word_number; k++)
                    node[k] = matrix[b, k];
                this.initialMatrix.AddFirst(node);
            }

            double[,] temp;
            //initial centroid learning and insert must link vectors to appropriate cluster
            for (int i = 0; i < this.clusterNumber; i++)
            {
                //Create instances of linked lists
                LinkedList<double[]> tmpMustlist = new LinkedList<double[]>();
                LinkedList<double[]> tmpCannotlist = new LinkedList<double[]>();
                LinkedList<double[]> tmpRelatedlist = new LinkedList<double[]>();

                this.clust[i].localMustLink = tmpMustlist;
                this.clust[i].localCannotLink = tmpCannotlist;
                this.clust[i].relatedPoints = tmpRelatedlist;
                
                //build must linked list per cluster
                temp = this.mustLinkConstraints.First.Value;
                //number of sites in each must link (2 by default)
                for (int b = 0; b < temp.GetLength(0); b++)
                {
                    double[] node = new double[word_number];
                    for (int k = 0; k < word_number; k++)
                        node[k] = temp[b, k];
                    //Update must link for cluster i
                    this.clust[i].localMustLink.AddFirst(node);
                }

                //assign learning matrix = I, for the first time
                this.clust[i].learningMatrix = Tools.AssignIMatr(this.word_number);
 
                //update centroids for the first time
                this.clust[i].clusterCentroid = Tools.updateCentroid(temp); 
                this.mustLinkConstraints.RemoveFirst();
            }

            //Update cannot link per cluster with all must link of other clusters
            for (int m = 0; m < this.clusterNumber; m++)
                for (int n = 0; n < this.clusterNumber; n++)
                    if (m != n)
                        foreach (double[] t in this.clust[n].localMustLink)
                            this.clust[m].localCannotLink.AddFirst(t);
            

            //begin iterative steps of algorithm
            iterativeSteps();

        }

        #endregion

        #region IterativeSteps

        public void iterativeSteps()
        {
            int convergence=1, max_itter = 100000;
            while ((convergence == 1) && (max_itter > 0))
            {
                //if there is no change in points assignment, convergence=0
                convergence = assignCluster();
                estimateMeans();
                //updateMetrics();
                max_itter--;
            }
            
        }

        public int assignCluster()
        {
            bool not_first_run=false;
            int converge = 1;
            double[] obj_func = new double [this.clusterNumber];
            int min_clust;
            int ALen=this.clust[0].learningMatrix.GetLength(0); //Len of matr A
            //scan all vectors
            foreach (double[] t in this.initialMatrix)
            {
                for (int i = 0; i < this.clusterNumber; i++)
                {
                    //calc obj func value [i] for every cluster [i] per data point [t]

                    //r = (Xi-Lh)
                    double[] r = Tools.Subtract(t, this.clust[i].clusterCentroid);
                    
                    //sqrt( r * A * r )
                    double multVecCentrA=Math.Sqrt(Tools.Multiply_vec_sc(Tools.MultiplyVecMatr(
                       r, this.clust[i].learningMatrix), r));
                    
                    //log(det(A))
                    //double logDetA = Math.Log10(Tools.det(this.clust[i].learningMatrix, ALen));

                    double must_viol =  must_viol_compute(i,t);

                    double cannot_viol =  cannot_viol_compute(i,t, not_first_run); 

                    obj_func[i] = multVecCentrA + must_viol + cannot_viol;
                }
                
                min_clust= Tools.MinIndex(obj_func);
                //go to this.clust[min_clust] and check if vector t in there, if it is, converge=0,quit
                //if is not, remove from old cluster, add to optimal cluster, converge=1.
                 if (this.clust[min_clust].relatedPoints.Contains(t))
                 {
                     converge = 0;
                 }
                 else
                 {
                     for (int i = 0; i < this.clusterNumber; i++)
                     {
                         if (this.clust[i].relatedPoints.Contains(t))
                             this.clust[i].relatedPoints.Remove(t);
                     }
                     this.clust[min_clust].relatedPoints.AddFirst(t);
                     converge = 1;
                 }

            }
            not_first_run = true;
            return converge;
        }

        public void estimateMeans()
        {
            for (int i = 0; i < this.clusterNumber; i++)
                this.clust[i].clusterCentroid = Tools.updateCentroid(this.clust[i].relatedPoints, word_number);
        }

        public void updateMetrics()
        {
            double[] sub_vect, subMaxSep;
            double[,] tempA, multMaxSep, multPoints; 
            double[,] tmpMustViol, mustViol=Tools.AssignZerMatr(this.word_number),
               tmpCannotViol, cannotViol = Tools.AssignZerMatr(this.word_number);
            //Loop for each cluster
            for (int i = 0; i < this.clusterNumber; i++)
            {
                //Assign matrix for each cluster
                double[,] resA = Tools.AssignZerMatr(this.word_number);
                foreach (double[] t in this.clust[i].relatedPoints)
                {
                    sub_vect = Tools.Subtract(t, this.clust[i].clusterCentroid);
                    tempA = Tools.Multiply(sub_vect, sub_vect);
                    resA = Tools.Add_Sub(tempA, resA,1);
                }

                

                //Must Link violation: Add only if MustLink point is not in related points.
                foreach (double[] p in this.clust[i].localMustLink)
                    //MustLink point is not in cluster
                    if (!(this.clust[i].relatedPoints.Contains(p)))
                        foreach (double[] tmp in this.clust[i].localMustLink)
                        {
                            if (!Tools.Equals(p, tmp))
                            {
                                sub_vect = Tools.Subtract(p, tmp);
                                tempA = Tools.Multiply(sub_vect, sub_vect);
                                tmpMustViol = Tools.Multiply(tempA, (0.5 * this.mustViolation));
                                mustViol = Tools.Add_Sub(mustViol, tmpMustViol,1);
                            }
                        }



                //Cannot Link violation: Add only if CannotLink point is in related points.
                foreach (double[] p in this.clust[i].localCannotLink)
                    //CannotLink point is in cluster
                    if (this.clust[i].relatedPoints.Contains(p))
                    {
                        subMaxSep = maxSepPoints(i);
                        multMaxSep = Tools.Multiply(subMaxSep, subMaxSep);
                        foreach (double[] tmp in this.clust[i].localCannotLink)
                        {
                            if (!Tools.Equals(p, tmp))
                            {
                                sub_vect = Tools.Subtract(p, tmp);
                                multPoints = Tools.Multiply(sub_vect, sub_vect);
                                tempA = Tools.Add_Sub(multMaxSep, multPoints, 0);
                                tmpCannotViol = Tools.Multiply(tempA, this.cannotViolation);
                                cannotViol = Tools.Add_Sub(cannotViol, tmpCannotViol, 1);
                            }
                        }
                    }


                //final multiply by point number in cluster
                resA = Tools.Add_Sub(resA, mustViol,1);
                resA = Tools.Add_Sub(resA, cannotViol,1);
                //resA = Accord.Math.Matrix.Inverse(resA);
                resA = Tools.Multiply(resA, this.clust[i].relatedPoints.Count);
                this.clust[i].learningMatrix = resA;
            }
        }
        #endregion

        #region Calculations

        //Calculate posible must link violation value per cluster
        public double must_viol_compute(int this_clust, double[] xi)
        {
            double viol = 0;
            //If "node" is "must link" in cluster j, increase obj_func for this_clust
            for (int j = 0; j < this.clusterNumber; j++)
            {
                if(j!=this_clust)
                  // if(this.clust[j].relatedPoints.Contains(xi))
                    foreach (double[] tmp in this.clust[j].localMustLink)
                        if(Tools.Equals(tmp,xi))
                            foreach (double[] xj in clust[j].localMustLink)
                                if (!(Tools.Equals(xj, xi)))
                                    viol += this.mustViolation * fm(xi, xj,
                                        clust[this_clust].learningMatrix, clust[j].learningMatrix);
            }
            return viol;
        }

        public double fm(double[] xi, double[] xj, double[,] Ai, double[,] Aj)
        {
            double fmRes=0;
            double[] subVec;
            subVec = Tools.Subtract(xi, xj);
            fmRes = 0.5 * (Math.Sqrt(Tools.Multiply_vec_sc(Tools.MultiplyVecMatr(subVec, Ai), subVec)));
            fmRes += 0.5 * (Math.Sqrt(Tools.Multiply_vec_sc(Tools.MultiplyVecMatr(subVec, Aj), subVec)));
            return fmRes;
        }

        //calculate posible cannot link violation value per cluster
        public double cannot_viol_compute(int this_clust, double[] xi, bool run)  
        {
            double cannot_viol = 0;
            //Cannot link per cluster contains all must link of other clusters
            //if (clust[this_clust].localCannotLink.Contains(xi))
             foreach (double[] tmp in this.clust[this_clust].localCannotLink)            
                 if(Tools.Equals(tmp,xi))
                    foreach (double[] xj in clust[this_clust].localCannotLink)
                        if (!(Tools.Equals(xj, xi)))
                            cannot_viol += this.cannotViolation * 
                                fc(xi, xj, clust[this_clust].learningMatrix, this_clust, run);
            return cannot_viol;
        }

        public double fc(double[] xi, double[] xj, double[,] Ai, int this_clust, bool not_first_run)
        {
            double fcRes = 0;
            double temp;
            double temp2;
            double[] subVecXiXj = Tools.Subtract(xi, xj);
            double[] maxSepPoint = maxSepPoints(this_clust);
            temp = Math.Sqrt(Tools.Multiply_vec_sc(Tools.MultiplyVecMatr(maxSepPoint, Ai), maxSepPoint));
            temp2=Math.Sqrt(Tools.Multiply_vec_sc(Tools.MultiplyVecMatr(subVecXiXj, Ai), subVecXiXj));
            if (not_first_run)
                fcRes = temp - temp2;
            else
                fcRes = temp2;
            return fcRes;
        }

        public double[] maxSepPoints(int this_clust)
        {
            double maxDist=0, tmpDist=0;
            double[] resSubPoints = new double[this.word_number];
            foreach (double[] tmp in this.clust[this_clust].relatedPoints)
            {
                foreach (double[] tmp2 in this.clust[this_clust].relatedPoints)
                    if (!Tools.Equals(tmp, tmp2))
                    {
                        tmpDist = Math.Sqrt(Tools.Multiply_vec_sc(
                            Tools.Subtract(tmp, tmp2), Tools.Subtract(tmp, tmp2)));
                        if (maxDist < tmpDist)
                        {
                            resSubPoints = Tools.Subtract(tmp, tmp2);
                            maxDist = tmpDist;
                        }
                    }
            }
            return resSubPoints;
        }

        #endregion

        public cluster[] getPoints()
        {
            return clust;
        }

        public int getClustNum()
        {
            return this.clusterNumber;
        }
    }
}
