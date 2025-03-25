# Git Workflow for Village Game

This document outlines the Git workflow for developing new features in the Village Game project.

## Branch Structure

The project uses the following branch structure:

- **main**: Production-ready code
- **testing**: Integration and testing branch
- **develop**: Development integration branch
- **feature branches**: Individual feature development under `dev/feature-name`

```
main
 ↑
testing
 ↑
develop
 ↑
dev/feature-1  dev/feature-2  dev/feature-3  ...
```

## Feature Development Process

### 1. Starting a New Feature

When starting work on a new feature:

```bash
# Make sure you're on the develop branch
git checkout develop

# Pull the latest changes
git pull origin develop

# Create a new feature branch
git checkout -b dev/feature-name
```

Feature branch naming convention: `dev/feature-name`

Examples:
- `dev/enemy-wave-system`
- `dev/tower-placement`
- `dev/resource-management`

### 2. Working on the Feature

- Implement the feature
- Write comprehensive unit tests
- Ensure all tests pass
- Make regular, atomic commits with clear messages

### 3. Creating a Pull Request

When the feature is complete:

1. Push the feature branch to GitHub:
   ```bash
   git push origin dev/feature-name
   ```

2. Create a pull request to the `testing` branch on GitHub
   - Include a detailed description of the feature
   - Reference any related issues

### 4. Testing Phase

- All code changes will be tested in the `testing` branch
- Code reviews and QA will be performed
- Any necessary changes will be addressed

### 5. Merging to Main

Once the feature is tested and verified:

1. The PR will be approved
2. The code will be merged into the `testing` branch
3. After final verification, `testing` will be merged into `main`

## Testing Requirements

For every feature:

- Unit tests must be written covering at least 80% of the new code
- Tests must pass before creating a PR
- Integration tests should be included where appropriate

## Continuous Integration

- GitHub Actions will automatically run tests on PR creation
- Code quality metrics will be evaluated
- Build verification will be performed

## Version Control Hygiene

- Keep commits focused and atomic
- Write clear commit messages
- Rebase feature branches on develop before PR
- Don't commit directly to `develop`, `testing`, or `main`

## PR Checklist

Before submitting a PR, ensure:

- [ ] Feature is complete and working as expected
- [ ] All tests are passing
- [ ] Code follows project standards
- [ ] Documentation is updated
- [ ] PR description clearly explains the changes

## Workflow Example

1. Start feature: `git checkout -b dev/enemy-animation`
2. Implement feature and write tests
3. Push to GitHub: `git push origin dev/enemy-animation`
4. Create PR to `testing` branch
5. Address review feedback
6. PR is merged to `testing` after approval
7. Verified changes from `testing` are merged to `main` 