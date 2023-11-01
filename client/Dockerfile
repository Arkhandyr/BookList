# Step 1: build
# Use a node version compatible with your Angular CLI.
# See: https://stackoverflow.com/a/60258560/2842142
FROM node:18 as node
RUN mkdir /booklist-front
WORKDIR /booklist-front
COPY . /booklist-front
RUN npm ci --legacy-peer-deps
# the CLI options differ with Angular versions
RUN npm run build -- --configuration production --output-path dist

# Step 2: serve
FROM nginx
COPY --from=node /booklist-front/dist/ /usr/share/nginx/html
