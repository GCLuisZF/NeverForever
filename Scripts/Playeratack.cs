#include<iostream>
#include<iomanip>
using  namespace  std;

int  main()
{        
        double  sum,an;
        int  i=1,n,k;
        cin>>n;  
        if(n<1)
        {
                cout<<"Input  error,end.";  
                return  1;
        }                    
        sum=0.0;
        k=1;
        while(i<=n)
        {        
                
an=1/(2*i-1);


                sum+=an*k;
                
k*=-1;
i++;



                    
        }
        cout<<setiosflags(ios::fixed)<<setprecision(6)<<"sum="<<sum<<endl;            
        return  0;
}
