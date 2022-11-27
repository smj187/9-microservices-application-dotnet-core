FROM node:16-alpine
WORKDIR /usr/src/app
COPY package.json ./
RUN npm i -g pnpm
RUN pnpm install --frozen-lockfile --prod
RUN pnpm install esbuild
COPY . .
EXPOSE 8080
CMD ["pnpm", "prod"]