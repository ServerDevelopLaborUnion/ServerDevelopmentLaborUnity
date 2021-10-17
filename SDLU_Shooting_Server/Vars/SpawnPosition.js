const { Vector3 } = require("../Utils/Vector3.js");

let spawnPositions = [];

spawnPositions.push(JSON.stringify(new Vector3(0, 0, 0)));
spawnPositions.push(JSON.stringify(new Vector3(10, 0, 0)));
spawnPositions.push(JSON.stringify(new Vector3(0, 10, 0)));
spawnPositions.push(JSON.stringify(new Vector3(0, 0, 10)));

exports.spawnPositions = spawnPositions;