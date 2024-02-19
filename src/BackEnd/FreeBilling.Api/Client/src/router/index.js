import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import LoginView from '../views/LoginView.vue'
import store from "../store";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    }
  ]
})

router.beforeEach((to, from) => {
  if (to.name !== "login") {
    if (!store.auth.isAuthenticated()) {
      console.log("not authenticated, redirecting to login");
      return { name: "login" };
    }
  }
  return true;
});

export default router
