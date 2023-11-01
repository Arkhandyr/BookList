FROM node:20-alpine AS build
WORKDIR /booklist

COPY src ./src/
COPY package*.json .
COPY angular.json .
COPY tsconfig*.json .

RUN npm cache clean --force
RUN npm install
RUN npm run build

FROM nginx:alpine

COPY --from=build /booklist/dist/booklist /usr/share/nginx/html

COPY ./nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 4200
