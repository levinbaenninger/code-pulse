import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { BlogPost } from '../../models/blog-post.model';
import { BlogPostService } from '../../services/blog-post.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
})
export class HomeComponent implements OnInit, OnDestroy {
  blogPosts?: BlogPost[];
  private getBlogPostsSubscription?: Subscription;

  constructor(private blogPostService: BlogPostService) {}

  ngOnInit(): void {
    this.getBlogPostsSubscription = this.blogPostService
      .getBlogPosts()
      .subscribe((blogPosts) => {
        this.blogPosts = blogPosts;
      });
  }

  ngOnDestroy(): void {
    this.getBlogPostsSubscription?.unsubscribe();
  }
}
