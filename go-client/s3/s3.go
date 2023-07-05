package s3

import (
	"context"
	"fmt"
	"log"

	"github.com/minio/minio-go/v7"
	"github.com/minio/minio-go/v7/pkg/credentials"
)

type (
	S3Service struct {
		client *minio.Client
		bucket string
	}
)

func NewS3Service() *S3Service {
	mc, err := minio.New("s3.belvedersky.ru", &minio.Options{
		Creds:  credentials.NewStaticV4("...", "...", ""),
		Secure: true,
	})
	if err != nil {
		panic(err)
	}

	return &S3Service{
		client: mc,
		bucket: "r1pro",
	}
}

func (s *S3Service) DownloadFileMinio() (*minio.Object, error) {

	obj, err := s.client.GetObject(context.Background(), s.bucket, "каеф.jpg", minio.GetObjectOptions{})
	if err != nil {
		return nil, err
	}

	// localFile, err := os.Create("C:\\Users\\...\\Desktop\\s3\\asd.jpg")
	// if err != nil {
	// 	return nil, err
	// }
	// defer localFile.Close()

	// if _, err = io.Copy(localFile, obj); err != nil {
	// 	return nil, err
	// }

	log.Println("Выгрузили из s3 кайф")

	return obj, nil
}

func (s *S3Service) UploadFileMinio(filename, filepath string) error {

	obj, err := s.client.FPutObject(context.Background(), s.bucket, filename, filepath, minio.PutObjectOptions{})
	if err != nil {
		return err
	}

	log.Printf("Загрузили на s3 кайф размер %d\n", obj.Size)

	return nil
}

func (s *S3Service) GetAllBucket() (buckets []string, err error) {

	res, err := s.client.ListBuckets(context.Background())
	if err != nil {
		return nil, err
	}

	for _, bucket := range res {
		fmt.Println(bucket)
		buckets = append(buckets, bucket.Name)
	}

	return buckets, nil
}
