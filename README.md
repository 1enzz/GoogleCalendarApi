
# Google Calendar API Integration - ASP.NET Core

This project is an ASP.NET Core Web API that integrates with the Google Calendar API, allowing events to be created programmatically via HTTP requests.

## Features

- OAuth 2.0 Authentication with Google
- Create calendar events dynamically using JSON input
- Fully working locally with support for testing via tools like [Ngrok](https://ngrok.com/)
- Clean architecture with separation of concerns
- Swagger UI for easy testing

## Technologies

- ASP.NET Core Web API
- Google.Apis.Calendar.v3
- OAuth 2.0 (via GoogleWebAuthorizationBroker)
- JSON input handling with System.Text.Json

---

## 📁 Project Structure

```

GoogleCalendarApi/
│
├── Controllers/
│   └── CalendarController.cs        # API endpoint to create calendar events
│
├── Models/
│   └── InformacoesProcesso.cs       # POCO class for input data
│
├── Services/
│   └── GoogleCalendarService.cs     # Business logic to authenticate and insert events
│
├── Credentials/
│   └── Credentials.json             # OAuth client credentials from Google Cloud Console
│
├── Program.cs
├── appsettings.json
└── token.json (generated on first login)

````

---

## 🛠️ How It Works

1. The app authenticates the user via Google's OAuth 2.0.
2. On first run, a browser window opens to authorize access.
3. Once access is granted, a `token.json` file is stored to avoid re-authentication.
4. An API call to `/api/calendar/criar-evento` with a valid JSON body creates an event on the authenticated Google Calendar.

---

## ✅ Example Request (Swagger)

POST `/api/calendar/criar-evento`

```json
{
  "processo": "Process ABC123",
  "localProcesso": "New York Office",
  "dataPublicacao": "01/05/2025",
  "dataFinal": "10/05/2025"
}
````

---

## 📌 Requirements

* .NET 6 or later
* Google Cloud Project with Calendar API enabled
* OAuth2 Client Credentials (`Credentials.json`)
* Internet access to complete OAuth login

---

## 🔐 OAuth & token.json

* The app uses `GoogleWebAuthorizationBroker` to open the OAuth consent screen and get the access token.
* The `token.json` file is automatically generated and stored locally to cache the access token and refresh token.
* To support multiple users, the app should dynamically store tokens using unique identifiers per user (e.g., email or user ID).

---

## 🌐 Using with Ngrok (for external testing)

1. Start the app locally:

   ```bash
   dotnet run
   ```

2. Run Ngrok to expose your local server (replace port 5092 if needed):

   ```bash
   ngrok http 5092
   ```

3. Copy the forwarding URL (e.g., `https://a1b2c3d4.ngrok.io`) and add it as an authorized redirect URI in your Google Cloud Console under "OAuth 2.0 Client IDs".

4. Test the API using Postman or Swagger via the Ngrok URL.

---

## 🚀 Future Improvements

* Support for multiple user tokens
* Endpoint to initiate OAuth flow and return login URL
* Use Dependency Injection for better testability
* Frontend integration for user login and calendar UI

---

## 📄 License

This project is licensed under the MIT License.


