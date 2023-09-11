import { User } from "./User";

export interface Review {
    _id: string;
    user_id: string;
    book_id: string;
    text: string;
    date: string;
    user: User;
    likes: string[];
  }