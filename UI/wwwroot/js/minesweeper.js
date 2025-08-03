document.addEventListener('DOMContentLoaded', function () {
    const board = document.getElementById('board');
    const gameId = board.dataset.gameid;

    board.addEventListener('click', function (e) {
        if (e.target.classList.contains('cell')) {
            handleCellClick(e.target, 'left', gameId);
        }
    });

    board.addEventListener('contextmenu', function (e) {
        e.preventDefault();
        if (e.target.classList.contains('cell')) {
            handleCellClick(e.target, 'right', gameId);
        }
    });
});

function handleCellClick(cell, clickType, gameId) {
    const row = cell.dataset.row;
    const col = cell.dataset.col;

    let url;
    if (clickType === 'right') {
        url = `/Game/ToggleFlag?gameId=${gameId}&row=${row}&col=${col}`;
    } else {
        url = `/Game/OpenCell?gameId=${gameId}&row=${row}&col=${col}`;
    }

    fetch(url, { method: 'POST' })
        .then(response => response.json())
        .then(data => {
            console.log(data);
            window.location.reload();
            if (data.status === "ended") {
                setSmiley(data.result === "win"); 
                const messageElement = document.getElementById("gameMessage");
                const textElement = document.getElementById("gameMessageText");
                if (data.result === "win") {
                    textElement.innerText = "🎉 Ви виграли!";
                    window.location.href = '/GameResults';
                } else {
                    textElement.innerText = "💣 Ви програли!";
                    window.location.href = '/GameResults';
                }

                messageElement.classList.remove("hidden");

                setTimeout(() => {
                    window.location.href = '/GameResults';
                }, 3000);
                return;
            }

            
            updateGameBoard(data);
            
            if (data.boardHtml) {
                const boardContainer = document.getElementById('board');
                boardContainer.innerHTML = data.boardHtml;
            }
        })
        .catch(err => console.error("Error:", err));
}
function updateGameBoard(data) {
    for (const cellData of data.cellsToUpdate) {
        const selector = `.cell[data-row="${cellData.row}"][data-col="${cellData.col}"]`;
        const cellElement = document.querySelector(selector);

        if (cellElement) {
            cellElement.textContent = cellData.displayValue;

            cellElement.classList.remove('flagged', 'closed');
            cellElement.classList.add(cellData.state);
        }
    }
}

function setSmiley(isWin) {
    const smiley = document.getElementById("smiley");
    if (!smiley) return;

    smiley.textContent = isWin ? "😎" : "😵";
}