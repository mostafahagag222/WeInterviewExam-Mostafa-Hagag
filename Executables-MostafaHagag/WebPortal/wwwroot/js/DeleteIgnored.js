document.addEventListener('DOMContentLoaded', function () {
    const table = document.getElementById('cuttingIgnoredTable');

    // Listen for clicks on delete buttons
    table.addEventListener('click', function (event) {
        if (event.target.classList.contains('delete-button')) {
            const id = event.target.getAttribute('data-id');

            // Confirm deletion
            if (!confirm('Are you sure you want to delete this cutting?')) {
                return;
            }

            // Call the API to delete the cutting
            fetch(`/Ignored/DeleteIgnoredCutting/${id}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            })
                .then(response => {
                    if (response.ok) {
                        // Remove the row from the table
                        const row = event.target.closest('tr');
                        row.remove();
                    } else {
                        return response.json().then(err => {
                            alert(`Error: ${err.message}`);
                        });
                    }
                })
                .catch(error => {
                    console.error('Error deleting cutting:', error);
                    alert('An error occurred while trying to delete the cutting.');
                });
        }
    });
});
