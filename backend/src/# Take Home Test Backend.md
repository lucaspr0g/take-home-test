# Take Home Test Backend

## Project Setup

### Prerequisites

- Node.js (v16 or higher recommended)
- npm (v8 or higher) or yarn
- (Optional) Docker & Docker Compose

### Installation

1. **Clone the repository:**
   ```sh
   git clone <repository-url>
   cd take-home-test/backend
   ```

2. **Install dependencies:**
   ```sh
   npm install
   # or
   yarn install
   ```

3. **Configure environment variables:**
   - Copy `.env.example` to `.env` and update values as needed.
   ```sh
   cp .env.example .env
   ```

4. **Database setup:**
   - Ensure your database is running and accessible.
   - Run migrations (if applicable):
     ```sh
     npm run migrate
     # or
     yarn migrate
     ```

### Running the Project

- **Start the development server:**
  ```sh
  npm run dev
  # or
  yarn dev
  ```

- **Start the production server:**
  ```sh
  npm run build
  npm start
  # or
  yarn build
  yarn start
  ```

### Testing

- **Run tests:**
  ```sh
  npm test
  # or
  yarn test
  ```

### Using Docker (Optional)

1. **Build and run with Docker Compose:**
   ```sh
   docker-compose up --build
   ```

2. **Stop containers:**
   ```sh
   docker-compose down
   ```

---

## Project Structure

- `src/` - Source code
- `tests/` - Test files
- `.env` - Environment variables

---

## Troubleshooting

- Ensure all environment variables are set correctly.
- Check database connectivity.
- Review logs for errors.

---

## License

This project is for take-home test purposes only.
