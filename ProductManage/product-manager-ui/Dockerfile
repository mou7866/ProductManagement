# Use an official Node.js runtime as a builder image
FROM node:18 as build

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .

RUN npm run build --prod

# Use Nginx to serve the built Angular app
FROM nginx:alpine

COPY --from=build /app/dist/product-manager-ui /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]