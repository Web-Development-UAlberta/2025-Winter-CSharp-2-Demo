#!/bin/bash

# Output file
output_file="dump.txt"

# Script name
script_name=$(basename "$0")

# Clear the output file if it exists
>"$output_file"

echo "Starting export..."

# Process excluded directories from arguments
excluded_dirs=()
for arg in "$@"; do
    # Remove trailing slash if present
    arg=${arg%/}
    excluded_dirs+=("$arg")
    echo "Will exclude directory: $arg"
done

# Combine tracked and untracked but not ignored files
(
    git ls-files
    git ls-files --others --exclude-standard
) | sort -u | while read -r file; do
    # Skip the output file itself
    if [ "$file" = "$output_file" ]; then
        continue
    fi

    # Skip the script itself
    if [ "$(basename "$file")" = "$script_name" ]; then
        echo "Skipping self: $file"
        continue
    fi

    # Check if file is in an excluded directory
    skip=false
    for dir in "${excluded_dirs[@]}"; do
        if [[ "$file" == "$dir"/* ]]; then
            echo "Skipping excluded directory file: $file"
            skip=true
            break
        fi
    done
    [ "$skip" = true ] && continue

    # Skip binary files
    if file -b --mime-encoding "$file" | grep -q binary; then
        echo "Skipping binary file: $file"
        continue
    fi

    # Show progress in console
    echo "Processing: $file"

    # Write the header with the file path
    echo "# ./$file" >>"$output_file"
    echo "" >>"$output_file"
    echo "\`\`\`" >>"$output_file"

    # Write the file contents with a timeout
    timeout 5s cat "$file" >>"$output_file" || {
        echo "Warning: Timeout processing $file - skipping"
        # Remove the partial entry
        sed -i '$ d' "$output_file" # Remove the last line
        continue
    }

    # Add newline before closing code block
    echo "" >>"$output_file"

    # Close the code block and add a blank line
    echo "\`\`\`" >>"$output_file"
    echo "" >>"$output_file"
done

echo "Export complete. Check $output_file for the results."
