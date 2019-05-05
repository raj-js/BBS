import { NbMenuItem } from '@nebular/theme';

export const MENU_ITEMS: NbMenuItem[] = [
  {
    title: 'E-commerce',
    icon: 'nb-e-commerce',
    link: '/pages/dashboard',
    home: true,
  },
  {
    title: 'IoT Dashboard',
    icon: 'nb-home',
    link: '/pages/iot-dashboard',
  },
  {
    title: '会员管理',
    icon: 'nb-person',
    link: '/pages/accounts/list',
  },
  {
    title: '文章管理',
    icon: 'nb-compose',
    children: [
      {
        title: '文章搜索',
        icon: 'nb-search',
        link: '/pages/articles/list',
      },
      {
        title: '待审核文章',
        icon: 'nb-edit',
        link: '/',
      },
      {
        title: '待审核评论',
        icon: 'nb-edit',
        link: '/',
      },
    ],
  },
  {
    title: '举报管理',
    icon: 'nb-danger',
  },
  {
    title: '分类管理',
    icon: 'nb-grid-a-outline',
    link: '/pages/category',
  },
  {
    title: '公告管理',
    icon: 'nb-notifications',
    link: '/',
  },
  {
    title: '签到管理',
    icon: 'nb-location',
    link: '/',
  },
  {
    title: '系统设置',
    icon: 'nb-gear',
    link: '/',
  },
];
