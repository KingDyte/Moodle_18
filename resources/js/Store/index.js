import Vuex from "vuex";
import courses from "@/Store/Modules/courses.js";
import users from "@/Store/Modules/users.js";
import general from "@/Store/Modules/general.js";
const debug = process.env.NODE_ENV !== 'production';

export default new Vuex.Store({
    modules: {
        general,
        courses,
        users,
    },
    strict: debug,
    plugins: []
})
