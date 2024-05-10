import { Category } from './category.model';

export interface BlogPost {
  id: string;
  title: string;
  description: string;
  content: string;
  featuredImageUrl: string;
  urlHandle: string;
  datePublished: string;
  author: string;
  isVisible: boolean;
  categories: Category[];
}
