📄 Deanta.XMLToPDFConverter

<div align="center"> <img src="https://github.com/user-attachments/assets/4f4e231e-5110-4c4f-9bd2-c990008bc27f" alt="Deanta Trends Banner" width="100%"/> </div>

A robust and modular XML-to-PDF processing system built with .NET. This solution consists of a Blazor Web front-end for file upload and PDF tracking, and a background processing engine powered by Hangfire for scalable and decoupled PDF generation.

🧠 High-Level Overview
The system is composed of two core parts:

🌐 Web Front-End (Blazor)
User-friendly interface to upload XML files

Performs DTD validation and displays detailed error messages

Provides a real-time dashboard to monitor all uploaded files and generated PDFs

Features include filtering by status, pagination, download links, and error insights

⚙️ FileProcessor Engine (Worker)
A standalone background service that functions as the processing engine for the system

Responsible for:

Validating XML files against DTDs

Parsing XML content and generating styled PDF documents

Logging errors and processing metadata

Integrated with Hangfire for reliable, asynchronous job queuing and monitoring

Fully decoupled from the Web UI, allowing any other interface (e.g., desktop app, REST API) to plug into the system seamlessly

This architecture promotes separation of concerns and ensures flexibility, scalability, and testability for future enhancements or integrations.

