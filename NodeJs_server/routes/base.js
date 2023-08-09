const express = require("express");
const router = express.Router();
const {
  getPlayer,
  addplayer,
  Updateplayer,
  getOnePlayer,
} = require("../controller/controller");

//router.route("/").get(intial);
router.route("/:id").get(getOnePlayer);
router.route("/").get(getPlayer);
router.route("/create").post(addplayer);
router.route("/update").put(Updateplayer);

module.exports = router;
