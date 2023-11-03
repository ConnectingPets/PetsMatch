import React from 'react';
import './CLabel.scss';

interface CLabelProps {
    inputName:string,
    title:string,
};

export const CLabel:React.FC<CLabelProps> = ({
    inputName,
    title,
}) => {
  return (
   <label className='form__label' htmlFor={inputName}>{title}</label>
  )
}
