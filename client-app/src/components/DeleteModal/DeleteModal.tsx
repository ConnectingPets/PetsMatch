import React from 'react';

import themeStore from '../../stores/themeStore';

import './DeleteModal.scss';

interface DeleteModalProps {
    subjectForDelete?: string,
    onDeleteOrCancelClick: () => void,
    onConfirmDelete: () => void
}

const DeleteModal: React.FC<DeleteModalProps> = ({ subjectForDelete, onDeleteOrCancelClick, onConfirmDelete }) => {
    return (
        <div className="delete">
            <div className={themeStore.isLightTheme ? 'delete__modal' : 'delete__modal delete__modal-dark'}>
                <div>
                    <p>Are you sure you want to delete <b>{subjectForDelete}</b>&nbsp; ?</p>
                    <button onClick={onDeleteOrCancelClick}>CANCEL</button>
                    <button onClick={onConfirmDelete}>Yes, DELETE</button>
                </div>
            </div>
        </div>
    );
};

export default DeleteModal;