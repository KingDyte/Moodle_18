const success = 'success';
const warning = 'warning';
const error = 'error';

const state = () => ({
    loggedUserDegree: {},
    loggedUserCourses: [],
    showNotification: false,
    notificationData: {
        type: '',
        title: '',
        content: ''
    }
});

const getters = {
    getLoggedUserDegree: (state) => {
        return state.loggedUserDegree;
    },
    getLoggedUserCourses: (state) => {
        return state.loggedUserCourses;
    }
};

const actions = {
    showSuccessNotification({commit}, context = {}) {
        const title = context.title ? context.title : 'Success';
        const content = context.content ? context.content : "Success action!";
        commit('updateNotificationData', {type: success, title, content});
        commit('toggleShowNotification', true);
    },
    showWarningNotification({commit}, context = {}) {
        const title = context.title ? context.title : 'Warning';
        const content = context.content ? context.content : "User hasn't upload permission!";
        commit('updateNotificationData', {type: warning, title, content});
        commit('toggleShowNotification', true);
    },
    showErrorNotification({commit}, context = {}) {
        const title = context.title ? context.title : 'Error';
        const content = context.content ? context.content : "Something wrong!";
        commit('updateNotificationData', {type: error, title, content});
        commit('toggleShowNotification', true);
    },
};

const mutations = {
    updateLoggedUser(state, loggedUser) {
        state.loggedUserDegree = loggedUser.degreeId;
        state.loggedUserCourses = loggedUser.courses;
    },
    updateNotificationData(state, data) {
        state.notificationData = data;
    },
    toggleShowNotification(state, status) {
        state.showNotification = status;
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}
