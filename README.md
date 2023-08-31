# Git Convention

## Commit Name (not a must but great to have)

### Convention

1. Please use **meaningful** commit name
2. Recommended to name commit with mostly lowercase alphabets
3. Naming convention: `<prefix>: <commit-name>`

### Prefixes

- `feat` Introduce a new feature
- `fix` Fix a bug (changing some logic)
- `style` Style user interface (not changing logic but appearance)
- `refactor` Refactor code (not changing logic and UI but codes structure)

## Git Commit

### Convention

1. Please don't commit any changes directly into branch `dev`
2. To develop new feature, please create new branch with **meaningful** name  
   Ex. `dev/<feature-name>` or `dev-<feature-name>`  
   _\*Make sure that you on the right branch for developing the feature_
3. When the feature is satisfied, make a **pull request** to branch `dev`

### Pull request cheat sheet

1. Go to [github.com/rubjob/2d-game](https://github.com/rubjob/2d-game)
2. Navigate to **`Pull Requests`** tab at the top
3. Click **`Create pull request`** button on the right
4. Make compare changes with **base**: `dev` &#8592; **compare**: `<your-branch-name>`
5. Put fancy title and description
6. Click **`Create pull request`**
