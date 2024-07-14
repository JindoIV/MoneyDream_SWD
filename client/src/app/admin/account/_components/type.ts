export type UserResponse = {
  totalPage: number;
  totalRecord: number;
  pageNumber: number;
  pageSize: number;
  pageData: UserModel[];
};

export type UserModel = {
  accountId: string;
  fullName: string;
  userName: string;
  age: number | null;
  gender: 'Male' | 'Female';
  dateofBirth: string;
  state: string | null;
  city: string | null;
  email: string;
  phoneNumber: string;
  picture: string | null;
  status: "ACTIVE" | "BLOCKED";
  created: string;
  roleId: number;
  notes: string | null;
  onBlock: () => void;
};

export type ConversationPendingModel = {
  conversationId: number;
  type: string;
  createAt: string;
  accountId: number;
  fullName: string;
  picture: string;
};
