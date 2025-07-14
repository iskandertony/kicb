REPO_NAME="kicb"
BRANCH_NAME="gh-pages"
BUILD_DIR="dist"
DEPLOY_DIR="deploy-temp"
STATIC_PATH="/$REPO_NAME"

echo "🧹 Удаляем старый dist..."  
rm -rf $BUILD_DIR

echo "📦 Публикация проекта..."
dotnet publish -c Release -o $BUILD_DIR

if [ $? -ne 0 ]; then
  echo "Ошибка при публикации. Скрипт остановлен."
  exit 1
fi

echo "🔧 Обновляем base href в index.html..."
sed -i '' 's|<base href="/" />|<base href="/kicb/" />|' $BUILD_DIR/wwwroot/index.html

rm -rf $DEPLOY_DIR

echo "Клонируем ветку $BRANCH_NAME..."
git clone -b $BRANCH_NAME --single-branch git@github.com:iskandertony/$REPO_NAME.git $DEPLOY_DIR

echo "🧹 Очищаем старое содержимое ветки $BRANCH_NAME..."
rm -rf $DEPLOY_DIR/*

echo "📂 Копируем новые файлы..."
cp -a $BUILD_DIR/wwwroot/. $DEPLOY_DIR/

touch $DEPLOY_DIR/.nojekyll

echo "Коммит и пуш в $BRANCH_NAME..."
cd $DEPLOY_DIR
git add .
git commit -m "Deploy Blazor app to GitHub Pages 🚀"
git push origin $BRANCH_NAME

cd ..
rm -rf $DEPLOY_DIR

echo "Деплой завершён! Проверьте: https://iskandertony.github.io/$REPO_NAME/"
