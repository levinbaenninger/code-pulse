import { Routes } from '@angular/router';
import { BlogPostDetailsComponent } from './components/blog-post/blog-post-details/blog-post-details.component';
import { BlogPostComponent } from './components/blog-post/blog-post.component';
import { blogPostRoutes } from './components/blog-post/blog-post.routes';
import { CategoryComponent } from './components/category/category.component';
import { categoryRoutes } from './components/category/category.routes';
import { ErrorComponent } from './components/error/error.component';
import { errorRoutes } from './components/error/error.routes';
import { HomeComponent } from './components/home/home.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'blog/:url',
    component: BlogPostDetailsComponent,
  },
  {
    path: 'admin/categories',
    component: CategoryComponent,
    children: categoryRoutes,
  },
  {
    path: 'admin/blog-posts',
    component: BlogPostComponent,
    children: blogPostRoutes,
  },
  {
    path: 'error',
    component: ErrorComponent,
    children: errorRoutes,
  },
  {
    path: '**',
    redirectTo: 'error/404',
  },
];
