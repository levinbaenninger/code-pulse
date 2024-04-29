import { Routes } from '@angular/router';
import { CategoryComponent } from './components/category/category.component';
import { categoryRoutes } from './components/category/category.routes';
import { ErrorComponent } from './components/error/error.component';
import { errorRoutes } from './components/error/error.routes';
import { BlogPostComponent } from './components/blog-post/blog-post.component';
import { blogPostRoutes } from './components/blog-post/blog-post.routes';

export const routes: Routes = [
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
