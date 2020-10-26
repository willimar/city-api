docker build -t cityapp-api .

heroku login
heroku container:login

docker tag cityapp-api registry.heroku.com/cityapp-api/web
docker push registry.heroku.com/cityapp-api/web

heroku container:release web -a cityapp-api