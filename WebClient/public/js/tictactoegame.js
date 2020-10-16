
var currentMove;
var currentTileIdx;
var moves = [];

function joinGame() {

    console.log('Contacting server...');

    var ticTacToeProxy = $.connection.ticTacToeHub;

    $.connection.hub.start()
        .done(function () { console.log('Connected'); })
        .fail(function () { console.log('Not Connected'); })
}

function makeMove(tile, x, y) {

    currentMove = { x, y };
    currentTileIdx = moves.findIndex(checkTile)

    if (currentTileIdx > -1) {
        moves.splice(currentTileIdx, 1);
        tile.src = "img/blank.png";
    }
    else if (moves.length < 3) {

        moves.push(currentMove);
        tile.src = "img/x.png";
    }

    console.log(moves.length);

    $(':button').prop('disabled', true);

    // sending move to server...


    $('#msg').text('Waiting for opponent...');
}

function checkTile(tileCoord) {
    return currentMove.x == tileCoord.x && currentMove.y == tileCoord.y;
}


$(function () {

    $('#joinLink').click(function () {
        $(this).hide();
        $('#msg').text('Joining game...');
        joinGame();
    })

});
