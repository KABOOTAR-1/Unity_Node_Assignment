const express = require("express");
const app = express();
const db = require("./db/connected");
const Task = require("./models/gameschema");
const router = require("./routes/base");
const error_handler = require("./middleware/error_handler");
const body_parser = require("body-parser");

require("dotenv").config();

app.use(express.json());
app.use(body_parser.json());
//app.use(cors);
app.use("/player", router);
app.use(error_handler);

const port = process.env.PORT || 4000;
const start = async () => {
  try {
    await db(process.env.MONGO_URI);
    app.listen(port, () => {
      console.log(`Listning on port :${port}`);
    });
  } catch (err) {
    console.log(err);
  }
};

start();
