import './bootstrap';
import '../css/app.css';

import { createApp, h } from 'vue';
import { createInertiaApp } from '@inertiajs/vue3';
import { resolvePageComponent } from 'laravel-vite-plugin/inertia-helpers';
import { ZiggyVue } from '../../vendor/tightenco/ziggy/dist/vue.m';
import Echo from 'laravel-echo'
import Pusher from "pusher-js"
import store from "@/Store/index.js";

const appName = import.meta.env.VITE_APP_NAME || 'Laravel';
window.Pusher = Pusher;

window.Echo = new Echo({
    broadcaster: 'pusher',
    key: import.meta.env.VITE_PUSHER_APP_KEY,
    wsHost: import.meta.env.VITE_PUSHER_HOST,
    authEndpoint: 'http://'+import.meta.env.VITE_PUSHER_HOST+'/broadcasting/auth',
    encrypted: true,
    forceTLS: false,
    wsPort: 6001,
    wssPort: 6001,
    cluster: import.meta.env.VITE_PUSHER_APP_CLUSTER,
    disableStats: true,
    enabledTransports: ['ws', 'wss'],
    auth: {
    }
})

createInertiaApp({
    title: (title) => `${title} - ${appName}`,
    resolve: (name) => resolvePageComponent(`./Pages/${name}.vue`, import.meta.glob('./Pages/**/*.vue')),
    setup({ el, App, props, plugin }) {
        return createApp({ render: () => h(App, props) })
            .use(plugin)
            .use(ZiggyVue, Ziggy)
            .mount(el);
    },
    progress: {
        color: '#4B5563',
    },
});
