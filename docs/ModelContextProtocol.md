# Model Context Protocol

## Overview

This document describes how the AI assistant should use the context protocol to understand and work on the Village Game project. The context protocol is implemented through a hierarchy of markdown files in the `docs/context` directory, with `MAIN_CONTEXT.md` as the root context file.

## Context Structure

### Root Context
- `MAIN_CONTEXT.md`: The primary context file that provides an overview of the entire project
  - Project goals and vision
  - Core features
  - Technical stack
  - Development practices
  - Platform considerations

### Feature Contexts
Located in `docs/context/features/`, each feature has its own context directory containing:
- `CONTEXT.md`: Describes the feature's requirements and specifications
- `SOLUTION.md`: Details the implementation approach
- `USAGE.md`: Explains how to use and test the feature

## How to Use the Context Protocol

### 1. Start with MAIN_CONTEXT.md
- Always begin by understanding the project overview from `MAIN_CONTEXT.md`
- Use this as the foundation for all development decisions
- Reference the technical stack and development practices sections when making implementation choices

### 2. Feature-Specific Work
When working on specific features:
1. Navigate to the relevant feature context in `docs/context/features/`
2. Read the feature's `CONTEXT.md` for requirements
3. Consult `SOLUTION.md` for implementation details
4. Reference `USAGE.md` for testing and usage patterns

### 3. Context Hierarchy
- Main context takes precedence over feature contexts
- Feature contexts should be interpreted within the constraints of the main context
- When contexts appear to conflict, main context guidelines should be followed

### 4. Making Decisions
When making development decisions:
1. Check if the decision is addressed in `MAIN_CONTEXT.md`
2. Look for relevant feature context files
3. Follow the project's technical stack and practices
4. If no context exists, ask the user for clarification

### 5. Updating Context
- The AI should NOT modify context files unless explicitly instructed
- If context appears outdated or contradictory, notify the user
- Suggest context updates when appropriate

## Context Protocol Rules

1. **Completeness**
   - Read all relevant context files before making significant changes
   - Don't assume context from partial information

2. **Consistency**
   - Ensure actions align with both main and feature contexts
   - Flag any inconsistencies between contexts

3. **Clarity**
   - Ask for clarification when context is ambiguous
   - Don't make assumptions about missing context

4. **Compliance**
   - Follow the technical stack specified in the context
   - Adhere to development practices outlined in the context

## Example Usage

### Good:
```
Before implementing a new feature:
1. Read MAIN_CONTEXT.md to understand project goals
2. Navigate to relevant feature context
3. Follow implementation guidelines from SOLUTION.md
4. Verify against USAGE.md requirements
```

### Bad:
```
- Making assumptions without checking context
- Implementing features not described in context
- Ignoring technical stack specifications
- Modifying context files without permission
```

## Context Maintenance

The context protocol is maintained by:
1. Project maintainers updating context files
2. AI following context guidelines
3. Regular review and updates of context documentation

The AI should help maintain context integrity by:
- Following context guidelines strictly
- Reporting context inconsistencies
- Suggesting context updates when needed
- Never modifying context files without explicit instruction 