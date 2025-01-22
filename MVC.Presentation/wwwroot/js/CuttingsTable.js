document.addEventListener('DOMContentLoaded', function () {
    const table = document.getElementById('CuttingsTable');

    table.addEventListener('click', async function (event) {
        if (event.target.classList.contains('btn-close')) {
            const id = event.target.getAttribute('data-id');

            if (!confirm('Are you sure you want to close this cutting?')) {
                return;
            }

            const response = await fetch(`/Cutting/CloseCutting/${id}`, {
                method: 'Put',
                headers: {'Content-Type': 'application/json'}
            })
            if (response.ok) {
                const button = event.target;
                button.disabled = true;
                button.classList.remove('bg-green-600');
                button.classList.add('bg-gray-400', 'cursor-not-allowed');

                const row = event.target.closest('tr');
                if (row) {
                    const isActiveCell = row.querySelector('td[data-column="is-active"]');
                    if (isActiveCell) {
                        isActiveCell.textContent = 'False';
                    }
                    const updatedUserCell = row.querySelector('td[data-column="updated-user-name"]');
                    if (updatedUserCell) {
                        updatedUserCell.textContent = 'Manual';
                    }
                    const updatedUserIdCell = row.querySelector('td[data-column="updated-user-id"]');
                    if (updatedUserIdCell) {
                        const data = await response.json();
                        updatedUserIdCell.textContent = data.updatedSystemUserId;
                    }
                    const actualEndDateCell = row.querySelector('td[data-column="actual-end-date"]');
                    if (actualEndDateCell) {
                        actualEndDateCell.textContent = new Date().toISOString().split('T')[0];
                    }

                }
                alert("Closed Successfully!");
            } else
                alert("error during closing");
        }
    });
});


document.addEventListener('DOMContentLoaded', function () {
    const table = document.getElementById('CuttingsTable');

    table.addEventListener('click', async function (event) {
        if (event.target.classList.contains('btn-close-white')) {
            const table = document.getElementById('CuttingsTable');
            const rows = table.querySelectorAll('tbody tr');
            const headerIds = [];
            const closeButtons = table.querySelectorAll('.btn-close'); // Select all "Close" buttons
            const closeAllButtons = table.querySelectorAll('.btn-close-white'); // Select all "Close All" buttons

            rows.forEach(row => {
                const idCell = row.querySelector('td[data-column="header-id"]');
                if (idCell) {
                    const headerId = idCell.textContent.trim();
                    if (headerId) {
                        headerIds.push(parseInt(headerId));
                    }
                }
            });

            if (!confirm('Are you sure you want to close all cuttings?')) {
                return;
            }

            const response = await fetch('/Cutting/CloseAllCuttings', {
                method: 'POST',
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify({headerIds: headerIds})
            });

            if (response.ok) {
                alert('All cuttings successfully closed.');
                const data = await response.json();
                for (const row of rows) {
                    const isActiveCell = row.querySelector('td[data-column="is-active"]');
                    if (isActiveCell && isActiveCell.textContent.trim().toLowerCase() === 'true') {
                        isActiveCell.textContent = 'False';

                        const updatedUserCell = row.querySelector('td[data-column="updated-user-name"]');
                        if (updatedUserCell) {
                            updatedUserCell.textContent = 'Manual';
                        }
                        const updatedUserIdCell = row.querySelector('td[data-column="updated-user-id"]');
                        if (updatedUserIdCell) {
                            updatedUserIdCell.textContent = data.updatedSystemUserId;
                        }
                        const actualEndDateCell = row.querySelector('td[data-column="actual-end-date"]');
                        if (actualEndDateCell) {
                            actualEndDateCell.textContent = new Date().toISOString().split('T')[0];
                        }
                    }
                }
                // Disable all "Close All" buttons
                closeAllButtons.forEach(button => {
                    button.disabled = true;
                    button.classList.remove('bg-white');
                    button.classList.add('bg-gray-400', 'cursor-not-allowed');
                });

                // Disable all individual "Close" buttons
                closeButtons.forEach(button => {
                    button.disabled = true;
                    button.classList.remove('bg-green-600');
                    button.classList.add('bg-gray-400', 'cursor-not-allowed');
                });
            } else {
                alert('Failed to close all cuttings. Please try again.');
            }
        }

    });
});

