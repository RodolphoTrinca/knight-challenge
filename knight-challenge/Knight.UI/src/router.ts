import { createMemoryHistory, createRouter } from 'vue-router'

import Knights from './components/Knights.vue'
import Heroes from './components/Heroes.vue'

const routes = [
  { path: '/', component: Knights },
  { path: '/heroes', component: Heroes },
]

const router = createRouter({
  history: createMemoryHistory(),
  routes,
})

export default router