import React from 'react';
import './CSubmitButton.scss';

interface CSubmitButtonProps {
    textContent:string,
}

export const CSubmitButton:React.FC<CSubmitButtonProps> = ({
    textContent
}) => {
  return (
    <input type="submit" value={textContent} />
  )
}
