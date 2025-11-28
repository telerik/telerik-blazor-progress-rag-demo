# Progress Agentic RAG + Telerik Blazor UI Demo

A comprehensive demo application showcasing the powerful integration between **Progress Agentic RAG AI-powered search capabilities** and **Telerik Blazor UI components**. This application demonstrates how to build intelligent, data-driven interfaces that combine enterprise-grade AI retrieval with beautiful, functional user interfaces.

[![Live Demo](https://img.shields.io/badge/Live%20Demo-Click%20Here-blue)](https://your-demo-url-here.com)

## 🚀 Product Links

- **[Progress Agentic RAG](https://www.progress.com/agentic-rag)** - Enterprise-grade AI-powered search and retrieval
- **[Telerik UI for Blazor](https://www.telerik.com/blazor-ui)** - Professional UI components for Blazor applications

## 📋 Overview

This demo application illustrates how to:

- Integrate Progress Agentic RAG with Blazor Server applications
- Build intelligent search experiences with AI-powered responses
- Create interactive data visualizations with AI-generated insights
- Develop conversational AI interfaces with streaming responses
- Generate customized content based on user inputs

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────────────────────┐
│                         Blazor Server Application                   │
├─────────────────────────────────────────────────────────────────────┤
│  ┌─────────────────┐  ┌─────────────────┐  ┌─────────────────────┐  │
│  │   Razor Pages   │  │ Shared Components │  │    Services       │  │
│  │                 │  │                 │  │                   │  │
│  │  - Index        │  │  - ChatMessage  │  │  - NucliaSearch   │  │
│  │  - AISearch     │  │  - SearchInput  │  │    Service        │  │
│  │  - Financial    │  │  - MarkdownContent│ │                   │  │
│  │    Analysis     │  │  - GradientLoader│  │                   │  │
│  │  - Knowledge    │  │  - DrawerComponent│ │                   │  │
│  │    Assistant    │  │  - ChatMessageBox│  │                   │  │
│  │  - AgenticRag   │  │                 │  │                   │  │
│  │    Value        │  │                 │  │                   │  │
│  └─────────────────┘  └─────────────────┘  └─────────────────────┘  │
├─────────────────────────────────────────────────────────────────────┤
│                       Telerik UI for Blazor                         │
│  ┌─────────────────────────────────────────────────────────────┐    │
│  │  Chat · Charts · Buttons · TextArea · ToggleButton · etc.  │    │
│  └─────────────────────────────────────────────────────────────┘    │
├─────────────────────────────────────────────────────────────────────┤
│                        Progress Nuclia SDK                          │
│  ┌─────────────────────────────────────────────────────────────┐    │
│  │  NucliaDbClient · AskAsync · AskStreamAsync · Search API    │    │
│  └─────────────────────────────────────────────────────────────┘    │
├─────────────────────────────────────────────────────────────────────┤
│                        Progress Agentic RAG                         │
│  ┌─────────────────────────────────────────────────────────────┐    │
│  │  Knowledge Boxes · AI Search · Streaming Responses          │    │
│  └─────────────────────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────────────────────┘
```

### Key Components

| Layer | Technology | Purpose |
|-------|------------|---------|
| **Frontend** | Blazor Server | Server-side rendering with real-time updates via SignalR |
| **UI Components** | Telerik UI for Blazor | Enterprise-grade UI components (Chat, Charts, Forms) |
| **AI Integration** | Progress Nuclia SDK | .NET client for Progress Agentic RAG APIs |
| **AI Backend** | Progress Agentic RAG | AI-powered knowledge retrieval and generation |

## 📱 Demo Pages

### 1. Home Page (`/`)
The landing page featuring a search bar and navigation to all demo experiences. Users can start exploring the AI capabilities directly from here.

### 2. Intelligent Search (`/ai-search`)
An AI-powered search interface that:
- Provides intelligent responses to natural language queries
- Supports streaming responses for real-time feedback
- Displays AI-generated answers with markdown formatting
- Offers popular search suggestions

### 3. Financial Analysis (`/finance-analysis`)
A conversational AI assistant for financial data analysis:
- Chat-based interface using Telerik Chat component
- AI-generated chart visualizations using Telerik Charts
- Financial insights for companies like Apple, Google, NVIDIA, and more
- Dynamic chart expansion and thumbnail previews

### 4. Knowledge Assistant (`/knowledge-assistant`)
A documentation assistant that:
- Answers questions about KendoReact and related technologies
- Provides streaming conversational responses
- Features a drawer navigation component
- Supports chat suggestions for quick queries

### 5. Agentic RAG Value Proposition (`/value-proposition`)
A dynamic value proposition generator that:
- Creates customized proposals based on user inputs
- Supports industry, company size, data types, and use case selection
- Generates AI-powered content with markdown formatting
- Demonstrates form-based AI interaction patterns

## 🛠️ Technology Stack

- **.NET 9.0** - Latest .NET framework
- **Blazor Server** - Server-side Blazor with SignalR
- **Telerik UI for Blazor 12.0.0** - Professional UI component suite
- **Progress Nuclia SDK 0.1.0-preview.9** - AI integration library

## 📦 Prerequisites

Before running this project, ensure you have:

1. **.NET 9.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/9.0)

2. **Telerik UI for Blazor License or Trial**
   - Visit [Telerik UI for Blazor](https://www.telerik.com/blazor-ui) to obtain a license
   - You can start with a [free trial](https://www.telerik.com/download-trial-file/v2/ui-for-blazor)
   - The trial provides full access to all components for 30 days

3. **Progress Agentic RAG Account**
   - Sign up at [Progress Agentic RAG](https://www.progress.com/agentic-rag)
   - Create Knowledge Boxes and obtain API keys

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/telerik/telerik-blazor-progress-rag-demo.git
cd telerik-blazor-progress-rag-demo
```

### 2. Configure NuGet Package Sources

The Telerik packages require the Telerik NuGet feed. Add it to your NuGet configuration:

```bash
dotnet nuget add source "https://nuget.telerik.com/v3/index.json" \
  --name "TelerikNuGet" \
  --username "<your-telerik-email>" \
  --password "<your-telerik-password>"
```

> **Note:** Replace `<your-telerik-email>` and `<your-telerik-password>` with your Telerik account credentials.

### 3. Configure Progress Agentic RAG

Update the `blazor-progress-rag-demo/appsettings.json` file with your Progress Agentic RAG credentials:

```json
{
  "NucliaDb": {
    "ZoneId": "europe-1",
    "KnowledgeBoxId": "<your-knowledge-box-id>",
    "ApiKey": "<your-api-key>"
  },
  "NucliaDbCharts": {
    "ZoneId": "aws-eu-central-1-1",
    "KnowledgeBoxId": "<your-charts-knowledge-box-id>",
    "ApiKey": "<your-charts-api-key>"
  },
  "NucliaDbVerse": {
    "ZoneId": "aws-us-east-2-1",
    "KnowledgeBoxId": "<your-verse-knowledge-box-id>",
    "ApiKey": "<your-verse-api-key>"
  }
}
```

> **Security Note:** For production deployments, use environment variables or user secrets instead of storing credentials in configuration files.

### 4. Restore Dependencies

Navigate to the project directory and restore dependencies:

```bash
cd blazor-progress-rag-demo
dotnet restore
```

### 5. Run the Application

```bash
dotnet run
```

The application will start on `https://localhost:5001` (or the port configured in your launch settings).

Alternatively, run from the repository root:

```bash
dotnet run --project blazor-progress-rag-demo
```

## 🔧 Configuration

### Environment Variables

For production deployments, configure the following environment variables:

| Variable | Description |
|----------|-------------|
| `NucliaDb__ZoneId` | Zone ID for the default Knowledge Box |
| `NucliaDb__KnowledgeBoxId` | Knowledge Box ID for general queries |
| `NucliaDb__ApiKey` | API Key for the default Knowledge Box |
| `NucliaDbCharts__ZoneId` | Zone ID for the charts Knowledge Box |
| `NucliaDbCharts__KnowledgeBoxId` | Knowledge Box ID for financial data |
| `NucliaDbCharts__ApiKey` | API Key for the charts Knowledge Box |
| `NucliaDbVerse__ZoneId` | Zone ID for the verse Knowledge Box |
| `NucliaDbVerse__KnowledgeBoxId` | Knowledge Box ID for documentation |
| `NucliaDbVerse__ApiKey` | API Key for the verse Knowledge Box |

### User Secrets (Development)

For local development, use .NET user secrets from the project directory:

```bash
cd blazor-progress-rag-demo
dotnet user-secrets init
dotnet user-secrets set "NucliaDb:ApiKey" "<your-api-key>"
dotnet user-secrets set "NucliaDb:KnowledgeBoxId" "<your-knowledge-box-id>"
# ... and so on for other secrets
```

## 📂 Project Structure

```
telerik-blazor-progress-rag-demo/
├── blazor-progress-rag-demo/
│   ├── Pages/
│   │   ├── Index.razor           # Home page
│   │   ├── AISearch.razor        # Intelligent search
│   │   ├── FinancialAnalysis.razor # Financial analysis chat
│   │   ├── KnowledgeAssistant.razor # Documentation assistant
│   │   └── AgenticRagValue.razor # Value proposition generator
│   ├── Shared/
│   │   ├── MainLayout.razor      # Main application layout
│   │   ├── ChatMessage.razor     # Chat message component
│   │   ├── ChatMessageBox.razor  # Chat input component
│   │   ├── SearchInput.razor     # Search input component
│   │   ├── MarkdownContent.razor # Markdown rendering
│   │   └── ...                   # Other shared components
│   ├── Services/
│   │   ├── NucliaSearchService.cs # Progress Agentic RAG integration
│   │   ├── ChartModels.cs        # Chart data models
│   │   └── Schemas.cs            # JSON schemas for AI responses
│   ├── wwwroot/                  # Static assets
│   ├── Program.cs                # Application entry point
│   └── appsettings.json          # Configuration
├── blazor-progress-rag-demo.sln  # Solution file
└── README.md                     # This file
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit issues and pull requests.

## 📄 License

This project is provided for demonstration purposes. Please refer to the individual product licenses:

- [Telerik UI for Blazor License](https://www.telerik.com/purchase/license-agreement/ui-for-blazor)
- [Progress Agentic RAG Terms](https://www.progress.com/legal/terms-of-service)

## 📞 Support

- **Telerik Support:** [Telerik Support Center](https://www.telerik.com/support/blazor-ui)
- **Progress Agentic RAG:** [Progress Support](https://www.progress.com/support)

---

Built with ❤️ using [Progress Agentic RAG](https://www.progress.com/agentic-rag) and [Telerik UI for Blazor](https://www.telerik.com/blazor-ui)
