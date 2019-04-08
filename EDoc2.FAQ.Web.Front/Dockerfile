FROM node AS builder

WORKDIR /app

COPY . .

RUN npm install && npm run build --prod

FROM nginx:stable-alpine

COPY --from=builder /app/dist/web /usr/share/nginx/html

EXPOSE 80

ENTRYPOINT ["nginx", "-g", "daemon off;"]