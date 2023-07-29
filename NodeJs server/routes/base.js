const express = require("express");
const router = express.Router();
const {
  getPlayer,
  addplayer,
  Updateplayer,
  getOnePlayer,
} = require("../controller/controller");

//router.route("/").get(intial);
router.route("/player/:id").get(getOnePlayer);
router.route("/player").get(getPlayer);
router.route("/player/create").post(addplayer);
router.route("/player/update").put(Updateplayer);

module.exports = router;
