const mongoose = require("mongoose");

const Gameschema = new mongoose.Schema({
  user_name: {
    type: String,
    required: [true, "user_name must be provided"],
    unique: true,
  },
  score: {
    type: Number,
    required: [true],
    default: 0,
  },
});

module.exports = mongoose.model("GameSC", Gameschema);
