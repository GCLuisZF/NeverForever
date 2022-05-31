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
 void moveUpdate(float elapsedTime)
        {
            if (_path == null)
            {
                return;
            }
            if (_path.Length <= _currentWaypoint)
            {
                stopMove();
                return;
            }
            nextMoveStep(elapsedTime);
        }

        private void nextMoveStep(float deltaTime)
        {
            if (deltaTime <= 0.0f)
                return;

            Vector3 pa = _path[_currentWaypoint] - _chara.GetPos();
            Vector3 step = pa.normalized * _moveSpeed * deltaTime;
            _moveDir = pa.normalized;
            bool needMoveNext = false;
            //moveDir.y = 0;
            Vector3 ignorePa = pa; //ignorePa.y = 0;
            if (_moveDir == Vector3.zero || ignorePa.magnitude < 0.001f)
            {
                needMoveNext = true;
            }
            else if (step.sqrMagnitude >= pa.sqrMagnitude)
            {
                // 直接设置到目的点，避免卡顿的机器也可以进行移动
                //Debug.Log("直接设置到目的点，避免卡顿的机器也可以进行移动");
                _chara.SetPos(_path[_currentWaypoint]);
                needMoveNext = true;
            }

            if (!needMoveNext)
            {
                if (_charCtrl != null)
                {
                    _chara.gameObject.transform.Translate(step, Space.World);
                }
                else
                {
                    _chara.SetPos(_chara.GetPos() + step);
                }
            }
            else
            {
                _currentWaypoint++;
                if (_currentWaypoint < _path.Length)
                {
                    if (_turnPath)
                    {
                        setDesRotation(_path[_currentWaypoint], false, 10f);
                    }

                    float costTime = pa.magnitude / _moveSpeed;
                    deltaTime -= costTime;
                    nextMoveStep(deltaTime);
                }
                else
                {
                    _path = null;
                    _currentWaypoint = 0;
                    onReachPathEnd();
                }
            }
        }
