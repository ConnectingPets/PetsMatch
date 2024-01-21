import React, { useEffect, useState } from 'react';
import { FieldInputProps } from 'react-final-form';

import { CLabel } from '../common/CLabel/CLabel';

import '../../global-styles/forms-images.scss';
import agent from '../../api/axiosAgent';
import userStore from '../../stores/userStore';
import { toast } from 'react-toastify';

interface UserPhotoProps {
    input: FieldInputProps<File, HTMLElement>
    initialValue: string | undefined
}

const UserPhoto: React.FC<UserPhotoProps> = ({ input, initialValue }) => {
    const [photo, setPhoto] = useState<string | undefined>(initialValue || undefined);

    useEffect(() => {
        if (initialValue) {
            setPhoto(initialValue);
        }
    }, [initialValue]);

    const handleFile = async (
        e: React.ChangeEvent<HTMLInputElement>,
        input: FieldInputProps<File, HTMLElement>
    ) => {
        const file = e.target.files?.[0];

        if (file) {
            const imageUrl = URL.createObjectURL(file);

            setPhoto(imageUrl);
            input.onChange(file);

            const formData = new FormData();

            formData.append('file', file);

            try {
                const result = await agent.apiPhotos.addUserPhoto(formData);

                if (result.isSuccess) {
                    userStore.setUser({...userStore.user!, PhotoUrl: URL.createObjectURL(file)}, userStore.authToken!);

                    toast.success(result.successMessage);
                } else {
                    toast.error(result.errorMessage);
                }
            } catch(err) {
                console.error(err);
            }
        }
    };

    const handleRemovePhoto = async (input: FieldInputProps<File, HTMLElement>) => {
        setPhoto(undefined);
        input.onChange(null);

        try {
            const result = await agent.apiPhotos.deleteUserPhoto();

            if (result.isSuccess) {
                userStore.setUser({...userStore.user!, PhotoUrl: undefined}, userStore.authToken!);

                toast.success(result.successMessage);
            } else {
                toast.error(result.errorMessage);
            }
        } catch(err) {
            console.error(err);
        }
    };

    return (
        <>
            {!photo && (
                <>
                    <CLabel inputName='Photo' title='Photo' />
                    <div id="fileInput">
                        <input type="file" accept="image/*" onChange={(e) => handleFile(e, input)} name='Photo' id='Photo' />
                        <p className="fakeFileInput" >Upload Photo</p>
                    </div>
                </>
            )}
            
            {photo && (
                <div id="images">
                    <div className="image-preview">
                        <img src={photo} alt="preview image" />
                        <button type="button" onClick={() => handleRemovePhoto(input)}>X</button>
                    </div>
                </div>
            )}
        </>
    );
};

export default UserPhoto;