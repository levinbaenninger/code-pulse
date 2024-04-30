export interface BlogPost {
  id: string;
  title: string;
  description: string;
  content: string;
  featuredImageUrl: string;
  urlHandle: string;
  datePublished: Date;
  author: string;
  isVisible: boolean;
}
