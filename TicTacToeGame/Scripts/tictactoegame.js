



$(function () {

    var game = $.connection.ticTacToeGameHub;;
    var currentMove;
    var currentTileIdx;
    var myMoves = [];
    var opponentsMoves = [];
    var gameId;
    var remove = null;

    var tiles = [
        { x: 0, y: 0, img: $('#img00'), btn: $('#btn00') },
        { x: 0, y: 1, img: $('#img01'), btn: $('#btn01') },
        { x: 0, y: 2, img: $('#img02'), btn: $('#btn02') },
        { x: 1, y: 0, img: $('#img10'), btn: $('#btn10') },
        { x: 1, y: 1, img: $('#img11'), btn: $('#btn11') },
        { x: 1, y: 2, img: $('#img12'), btn: $('#btn12') },
        { x: 2, y: 0, img: $('#img20'), btn: $('#btn20') },
        { x: 2, y: 1, img: $('#img21'), btn: $('#btn21') },
        { x: 2, y: 2, img: $('#img22'), btn: $('#btn22') },
    ];

    game.client.gameCreated = function (id, myTurn) {
        gameId = id;
        updateBoard(myTurn);
    };

    game.client.opponentMove = function (newTile, oldTile) {
        if (oldTile != null) {
            opponentsMoves.splice(opponentsMoves.findIndex(function (m) {
                return m.x == oldTile.x && m.y == oldTile.y;
            }), 1);
        }
        opponentsMoves.push(newTile);
        updateBoard(true);
    };

    game.client.endGame = function (winner){

        if (winner) {

            $('#msg').text('You win...');
        }
        else {

            $('#msg').text('You loose...');
        }
        $(':button').prop('disabled', true);
    }

    $.connection.hub.start().done(function () {
        $('#joinLink').click(function () {
            $(this).hide();
            // join game on server
            game.server.join();
            updateBoard();
        })
    });

    makeMove = function (x, y) {

        currentMove = { x, y };
        currentTileIdx = myMoves.findIndex(checkTile)

        if (currentTileIdx > -1) { // removing previously selected tile
            myMoves.splice(currentTileIdx, 1);
            remove = currentMove;
            updateBoard(true);
        }
        else if (myMoves.length < 3) {
            myMoves.push(currentMove);
            updateBoard(false);
            // sending move to server...
            game.server.move(gameId, { x, y }, remove);
            remove = null;
        }
    };

    updateBoard = function (myTurn) {

        for (var i = 0; i < tiles.length; i++) {

            var tile = tiles[i];
 
            if (opponentsMoves.find(function (t) { return t.x == tile.x && t.y == tile.y; })) {
               tile.img.attr('src', '../images/o.png');
            }
            else if (myMoves.find(function (t) { return t.x == tile.x && t.y == tile.y; })) {
                tile.img.attr('src', '../images/x.png');
            }
            else {
                tile.img.attr('src', '../images/blank.png');
            }
        }

        if (myTurn == null) {
            $(':button').prop('disabled', true);
            $('#msg').text('Waiting for opponent to join the game...');
        }
        else if (myTurn) {
            $('#msg').text('Your move...')
            $(':button').prop('disabled', false);

            // disable opponents tiles
            for (var i = 0; i < opponentsMoves.length; i++) {
                oppTile = tiles.find(function (t) { return t.x == opponentsMoves[i].x && t.y == opponentsMoves[i].y });
                oppTile.btn.prop('disabled', true);
            }

            // disable currently removed tile
            if (remove != null) {
                remTile = tiles.find(function (t) { return t.x == remove.x && t.y == remove.y })
                remTile.btn.prop('disabled', true);
            }
        }
        else {
            $('#msg').text('Waiting for opponent...');
            $(':button').prop('disabled', true);
        }
    };

    checkTile = function (tileCoord) {
        return currentMove.x == tileCoord.x && currentMove.y == tileCoord.y;
    };
});
