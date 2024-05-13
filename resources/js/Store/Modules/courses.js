import axios from "axios";
import store from "@/Store/index.js";
const state = () => ({
    courses: [],
    degrees: [],
    selectedDegree: null,
    selectedDegreeName: null,
    selectedCourse: null,
    isMy: false,
    existsCourses: [],
    showList: false,
    errors: {},
});

const getters = {
    isVisible(state) {
        return state.showList;
    },
    getCourses(state) {
        return state.courses;
    },
    getExistsCourses(state) {
        return state.existsCourses;
    },
    getDegrees(state) {
        return state.degrees;
    },
    getSelectedDegreeName(state) {
        return state.selectedDegreeName;
    },
    getIsMy(state) {
        return state.isMy;
    },
};

const actions = {
    getCourses({commit, state, getters}, context) {
        let params = {isMy:0};
        if (state.selectedDegree) {
            params.degreeId = state.selectedDegree;
        }
        if (state.isMy) {
            params.isMy = 1;
        }
        return axios.get(route('courses.list'), {
            params: params
        }).then(res => {
            commit('updateCourses', res.data);
            return res;
        }).catch(e => {
            console.error(e);
        });
    },
    apply({commit, state, getters}, context) {
        if (!state.selectedCourse) {
            return;
        }
        return axios.post(route('courses.apply'), {
            courseId: state.selectedCourse
        }).then(res => {
            commit('selectCourse', null);
            store.dispatch('courses/getCourses');
            return res;
        }).catch(e => {
            store.dispatch('general/showErrorNotification', {
                title: 'Hibás kérés',
                content: 'A kiválasztott kurzusra nem jelentkezhedsz!'
            });
        });
    },
};

const mutations = {
    updateCourses(state, data) {
        state.showList = true;
        state.courses = data.courses;
        state.existsCourses = data.existsCourses;
        state.degrees = data.degrees;
    },
    selectDegree(state, degree) {
        state.selectedDegree = degree.id;
        state.selectedDegreeName = degree.name;
    },
    selectCourse(state, course) {
        state.selectedCourse = course;
    },
    setIsMy(state, isMy) {
        state.isMy = isMy;
    },
    updateErrors(state, errors) {
        for (const key in errors) {
            state.errors[key] = errors[key][0];
        }
    },
    resetErrors(state) {
        state.errors = {};
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
