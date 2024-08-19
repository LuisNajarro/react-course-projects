import React from 'react';

import TodoItem from './TodoItem.tsx';
import Todo from '../models/todo.ts';
import classes from './Todos.module.css';

const Todos: React.FC<{ items: Todo[] }> = (props) => {
  return (
    <ul className={classes.todos}>
      {props.items.map((item) => (
        <TodoItem key={item.id} text={item.text} />
      ))}
    </ul>
  );
};

export default Todos;
