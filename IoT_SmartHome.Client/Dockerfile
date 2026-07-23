FROM node:20-alpine
WORKDIR /app

COPY package*.json ./
RUN npm install

# Copy source code (make sure node_modules is ignored via .dockerignore)
COPY . .

EXPOSE 4200
CMD ["npm", "start", "--", "--host", "0.0.0.0"]