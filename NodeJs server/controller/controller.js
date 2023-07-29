const GameSC = require("../models/gameschema");
const try_catch = require("../middleware/try_catch");

const getPlayer = try_catch(async (req, res) => {
  const player = await GameSC.find({}).sort({ score: -1 }).limit(5);
  res.send(player);
});

const getOnePlayer = try_catch(async (req, res) => {
  const player = await GameSC.find({ user_name: req.params.id }).exec();
  if (player.length == 0) {
    throw new Error({ msg: "No such username exsists" });
  }

  res.send(player);
});

const addplayer = try_catch(async (req, res) => {
  const { user_name, score } = req.body;
  const newPlayer = await GameSC.create({ user_name, score });
  res.send(newPlayer);
});

const Updateplayer = try_catch(async (req, res) => {
  const task = req.body.user_name;

  const player = await GameSC.findOne({ user_name: task });
  if (player.score < req.body.score) {
    const find = await GameSC.findOneAndUpdate(
      { user_name: task },
      { score: req.body.score }
    );
    res.send(find);
  } else {
    res.send(player);
  }
});

module.exports = { getPlayer, addplayer, Updateplayer, getOnePlayer };
