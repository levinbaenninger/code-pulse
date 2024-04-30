import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Subscription } from 'rxjs';
import { BlogPost } from '../../../models/blog-post.model';
import { BlogPostService } from '../../../services/blog-post.service';

@Component({
  selector: 'app-blog-post-list',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './blog-post-list.component.html',
  styleUrl: './blog-post-list.component.scss',
})
export class BlogPostListComponent implements OnInit, OnDestroy {
  blogPosts!: BlogPost[];
  private getCategoriesSubscription?: Subscription;

  constructor(private blogPostService: BlogPostService) {}

  ngOnInit(): void {
    this.getCategoriesSubscription = this.blogPostService
      .getBlogPosts()
      .subscribe((blogPosts) => {
        this.blogPosts = blogPosts;
      });
  }

  ngOnDestroy(): void {
    this.getCategoriesSubscription?.unsubscribe();
  }
}
