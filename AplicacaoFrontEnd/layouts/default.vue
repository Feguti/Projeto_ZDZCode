<template>
  <v-app dark>
    <v-navigation-drawer v-model="drawer" :mini-variant="miniVariant" :clipped="clipped" fixed app color="#AC0B0B">
      <v-list>
        <v-list-item v-for="(item, i) in items" :key="i" :to="item.to" router exact>
          <v-list-item-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar :clipped-left="clipped" fixed app color="#DC0B0B">
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-btn icon @click.stop="miniVariant = !miniVariant">
        <v-icon>mdi-{{ `chevron-${miniVariant ? 'right' : 'left'}` }}</v-icon>
      </v-btn>
      <v-btn icon @click.stop="clipped = !clipped">
        <v-icon>mdi-application</v-icon>
      </v-btn>
      <v-btn icon @click.stop="fixed = !fixed">
        <v-icon>mdi-minus</v-icon>
      </v-btn>
      <v-spacer></v-spacer>
      <router-link to="/" class="title-link" style="text-decoration: none;">
        <v-toolbar-title class="custom-title">{{ title }}</v-toolbar-title>
      </router-link>
      <v-spacer></v-spacer>

      <v-btn icon @click.stop="rightDrawer = !rightDrawer">

        <v-icon>mdi-account</v-icon>
      </v-btn>
    </v-app-bar>
    <v-main>
      <v-container>
        <Nuxt />
      </v-container>
    </v-main>
    <v-navigation-drawer v-model="rightDrawer" :right="right" temporary fixed color="#AC0B0B">
      <v-list>
        <v-list-item>
          <v-list-item-title>Usuário Administrador</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-footer :absolute="!fixed" app>
      <span>&copy; {{ new Date().getFullYear() }}</span>
    </v-footer>
  </v-app>
</template>

<script>
import {
  mdiAccount,
} from '@mdi/js'
import { RouterLink } from 'vue-router';

export default {
  name: 'DefaultLayout',
  data() {
    return {
      clipped: false,
      drawer: false,
      fixed: false,
      items: [
        {
          icon: 'mdi-apps',
          title: 'Página Inicial',
          to: '/'
        },
        {
          icon: 'mdi-chart-bubble',
          title: 'Gerenciar funcionários',
          to: '/gerenciarFun'
        },
        {
          icon: 'mdi-chart-bubble',
          title: 'Gerenciar produtos',
          to: '/gerenciarProd'
        }
      ],
      miniVariant: false,
      right: true,
      rightDrawer: false,
      title: 'Kitsune Pharmacy',
      mdiAccount,
    }
  }
}
</script>

<style scoped>
@import url('https://fonts.cdnfonts.com/css/gagalin');


.custom-title {
  font-family: 'Gagalin', sans-serif;
  font-size: 28px;
  text-decoration: none;
  color: white;
}

</style>
