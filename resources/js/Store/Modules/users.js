import axios from "axios";
import store from "@/Store/index.js";
const state = () => ({
    selectedCourse: [],
    course: [],
    users: [],
    showList: false,
    errors: {},
});

const getters = {
    isVisible(state) {
        return state.showList;
    },
    getCourse(state) {
        return state.course;
    },
    getUsers(state) {
        return state.users;
    },
};

const actions = {
    getUsers({commit, state, getters}, context) {
        return axios.get(route('courses.users', state.selectedCourse), {
        }).then(res => {
            commit('updateUsers', res.data);
            return res;
        }).catch(e => {
            console.error(e);
        });
    },
};

const mutations = {
    updateUsers(state, data) {
        state.showList = true;
        state.course = data.course;
        state.users = data.users;
    },
    selectCourse(state, course) {
        state.selectedCourse = course;
    },
    toggleShowList(state, status) {
        state.showList = status;
    }
};

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
