# DDFilm

DDFilm is a web application that allows users to watch and rate movies together in real time. The platform enables the creation of viewing groups where each user can start a session, add movies, and provide ratings. Movies for viewing are selected randomly based on a custom algorithm, and all session data is stored in a database to maintain a history of watched films even after a session has ended.

> **Live Demo:**  
> [DDFilm Live Application](https://ddfilmclient-adbcbwega3chb4ca.polandcentral-01.azurewebsites.net)

---

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Deployment](#deployment)

---

## Overview

DDFilm is designed to simplify the process of browsing and rating movies by offering a collaborative viewing experience directly from the browser. The application supports real-time interactions using SignalR and WebRTC for streaming, ensuring smooth and synchronized movie sessions.

---

## Features

- **Real-Time Collaboration:**  
  Users can create groups for collective movie viewing and rating in real time.
  
- **Session Management:**  
  - Create sessions where movies can be added and rated.
  - Movies are chosen randomly based on a custom selection algorithm.
  - Session history is preserved in the database, allowing users to review past sessions.

- **Administrative Controls:**  
  - The session creator serves as the administrator, with privileges to modify or delete the session.
  - Session owners can enable screen sharing to broadcast a selected movie to all users during the session.

---

## Architecture

The backend of DDFilm is built on **Clean Architecture** principles and leverages **Domain-Driven Design (DDD)** to ensure a robust, scalable, and maintainable codebase. Key architectural elements include:

- **Clean Architecture:**  
  Separation of concerns across different layers (presentation, application, domain, and infrastructure) to enhance testability and flexibility.

- **Domain-Driven Design (DDD):**  
  Focus on the domain model and core business logic ensures that the application is centered around user needs.

- **Real-Time Communication:**  
  - **SignalR:** Provides real-time notifications and data synchronization between clients.
  - **WebRTC:** Supports live streaming, enabling users to watch movies simultaneously.

---

## Tech Stack

### Backend
- **Language:** C#
- **Framework:** .NET, ASP.NET Core
- **Database:** SQL/MSSQL
- **ORM:** EntityFramework
- **Real-Time Communication:** SignalR
- **Architecture:** Clean Architecture, DDD, CQRS, Result Pattern
- **Additional Libraries:** MediatR, JWT, Serilog, Mapster, FluentValidation

### Frontend
- **Languages:** JavaScript, TypeScript
- **Framework:** React
- **UI Technologies:** CSS, HTML, Tailwind CSS
- **Real-Time Communication:** SignalR, WebRTC
- **HTTP Client:** Axios
- **Utilities:** Toast, Yup

---

## Deployment

The DDFilm application is deployed on **Microsoft Azure** using Azure Web App Services. The deployment leverages Azure’s cloud infrastructure to ensure scalability and high availability.  
