# Photo & Cloud Manager Application Development Plan
This document outlines a detailed development plan for your C# WPF application, breaking down the project into manageable phases with specific tasks. You can use this as a checklist to track your progress.

Key best practices for building a scalable, maintainable WPF application using .NET 8 or later.

## 1. Target Modern .NET
- Use **.NET 8 (LTS)** or .NET 9 for performance and modern features.
- Avoid legacy .NET Framework.
- Create projects in Visual Studio 2022+.

## 2. Adopt MVVM Architecture
- Use **Model-View-ViewModel (MVVM)** for separation of concerns and testability.
- Leverage frameworks like **CommunityToolkit.Mvvm**, **Prism**, or **Caliburn.Micro**.

## 3. Modern Data Access
- Use **Entity Framework Core** for robust ORM or **Dapper** for lightweight, high-performance data access.
- Avoid LINQ to SQL (legacy, SQL Server only).
- Use async queries for responsiveness.

## 4. Dependency Injection (DI)
- Implement DI with **Microsoft.Extensions.DependencyInjection**, **Autofac**, or **Prism**.
- Register services in `App.xaml.cs` or a bootstrapper.

## 5. Clean XAML
- Use data binding, styles, and resource dictionaries for consistent UI.
- Create reusable **UserControls** or use libraries like **MaterialDesignInXamlToolkit**.

## 6. Responsive UI with Async/Await
- Use `async`/`await` for I/O operations to prevent UI freezes.
- Update UI collections on the Dispatcher.

## 7. Error Handling and Logging
- Use **Serilog**, **NLog**, or **Microsoft.Extensions.Logging**.
- Handle global exceptions in `App.xaml.cs`.

## 8. Unit Testing
- Test ViewModels and logic with **xUnit**, **NUnit**, or **MSTest**.
- Mock dependencies using **Moq** or **NSubstitute**.

## 9. Modern UI/UX
- Enhance UI with **MaterialDesignInXamlToolkit** or **MahApps.Metro**.
- Support light/dark themes and accessibility.

## 10. Project Structure
- Organize into `Models`, `ViewModels`, `Views`, `Services`, `Helpers`, `Resources`.
- Use meaningful namespaces and `appsettings.json` for configuration.

## 11. Performance Optimization
- Enable virtualization for large data sets in `DataGrid` or `ListView`.
- Batch UI updates and use profiling tools.

## 12. CI/CD
- Set up pipelines with **GitHub Actions**, **Azure DevOps**, or **Jenkins**.
- Package apps as **MSIX** or **ClickOnce**.

## 13. Security
- Use parameterized queries to prevent SQL injection.
- Store sensitive data securely (e.g., **Windows Credential Manager**).
- Validate user inputs.

## 14. Cross-Platform Readiness
- Share logic in a .NET 8/Standard library for potential reuse.
- Abstract platform-specific services with DI.

## 15. Tooling
- Use **Visual Studio 2022** or **JetBrains Rider**.
- Enable **nullable reference types** and **C# 12** features.
- Use **Roslyn Analyzers** for code quality.

---

# Phases of development

## Phase 1: Local File Management Foundation
---
Goal: Build the core functionality for browsing and displaying local photos and videos.

Tasks:
[ ] Project Setup:

[ ] Create a new WPF Application project in Visual Studio.

[ ] Establish a clean folder structure (Models, ViewModels, Views, Services, Controls).

[ ] Data Models:

[ ] Create a Photo.cs Model class with properties like FilePath, FileName, CreationDate, and ThumbnailPath.

[ ] Create a PhotoViewModel.cs that wraps the Photo model and implements INotifyPropertyChanged to enable data binding.

[ ] File Scanning Service:

[ ] Create a FileService.cs class.

[ ] Implement an async method, GetPhotosAsync(string directoryPath), that scans a directory for image files.

[ ] Use Task.Run() to perform the file I/O and thumbnail generation on a background thread, preventing the UI from freezing.

[ ] UI & Data Binding:

[ ] Create GalleryView.xaml with an ItemsControl or ListView bound to an ObservableCollection<PhotoViewModel>.

[ ] Define a DataTemplate in XAML to display each photo as a thumbnail.

[ ] Initial UI Responsiveness:

[ ] Ensure the UI remains responsive while the GetPhotosAsync method is running.

[ ] Use Application.Current.Dispatcher.Invoke() to safely add new items to the ObservableCollection from the background thread, ensuring smooth updates.

[ ] Implement a simple loading animation or progress indicator while the scan is in progress.

## Phase 2: Cloud Storage Integration
---
Goal: Add a cloud provider and handle network-based operations.

Tasks:
[ ] API Client & Service:

[ ] Choose a cloud provider (e.g., Dropbox) and add its SDK via NuGet.

[ ] Create a CloudStorageService.cs class.

[ ] Implement async methods: ConnectAsync(), UploadFileAsync(string localPath), DownloadFileAsync(string cloudPath), and ListFilesAsync().

[ ] Cloud ViewModel:

[ ] Create a CloudViewModel.cs to manage the cloud-related logic.

[ ] Add ICommand properties for "Connect," "Upload," and "Sync."

[ ] Include a property for connection status (IsConnected).

[ ] Maintain an ObservableCollection of cloud file metadata.

[ ] UI for Cloud Operations:

[ ] Create a CloudView.xaml or integrate cloud controls into the main window.

[ ] Bind buttons to the commands in the CloudViewModel.

[ ] Display a list of files from the cloud using a ListView.

[ ] Synchronization Logic:

[ ] Implement the async SyncCommand in the CloudViewModel.

[ ] Inside the command, call CloudStorageService.ListFilesAsync() to get the cloud manifest.

[ ] Compare local and cloud file lists.

[ ] Use Task.WhenAll() to concurrently run multiple UploadFileAsync() or DownloadFileAsync() tasks, maximizing efficiency.

[ ] Progress Indicators:

[ ] Implement a mechanism to track the progress of uploads and downloads.

[ ] Bind progress bars in the UI to a progress property on a transfer object.

## Phase 3: Advanced Features & Polish
---
Goal: Add professional features for a robust and polished application.

Tasks:
[ ] Configuration & Persistence:

[ ] Use JSON serialization to save user settings, including local directories and cloud tokens.

[ ] Implement async methods for saving and loading the configuration file.

[ ] Advanced UI/UX:

[ ] Add drag-and-drop functionality to the GalleryView to enable easy uploading.

[ ] Implement a photo viewer window or control that shows a full-size image.

[ ] Add robust error messages and notifications to inform the user of network issues or failed operations.

[ ] Testing & Refactoring:

[ ] Write unit tests for your ViewModels and Services.

[ ] Refactor your code to improve readability and maintainability.

[ ] Ensure all async methods are properly awaited and exceptions are handled.
