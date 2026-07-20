# Use Node.js to run the Angular dev server
FROM node:20-alpine

WORKDIR /app

# Install dependencies
COPY package*.json ./
RUN npm install

# Copy project files
COPY . .

EXPOSE 4200

# Host 0.0.0.0 is necessary to expose Angular outside the container
CMD ["npm", "start", "--", "--host", "0.0.0.0"]