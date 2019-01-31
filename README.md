# TestSchool
The steps of how to update\commit\build\deploy the project
1. udpate code   
2. git add .   
3. git commit -m "message"  
4. git push origin master 
5. git pull -----url on lunix machine
6. docker build -t yubao/testschool
7. docker push yubao/testschool (maybe you need login first docker login)
8. docker pull yubao/testschool
9. docker tag 0e5574283393 fedora/httpd:version1.0  or 
   docker tag httpd fedora/httpd:version1.0
